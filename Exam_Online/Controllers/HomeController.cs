using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Exam_Online.Models;
using Exam_Online.Data;
using Exam_Online.Models.IndexModel;
using Microsoft.AspNetCore.Http;
using Exam_Online.Models.Question;
using Microsoft.EntityFrameworkCore;

namespace Exam_Online.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IHttpContextAccessor _contextAccesor;

        private const string SessionKeyName = "TOKEN";

        private const string SessionKeyAge = "TOKENEXPIRE";

        public HomeController(ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccesor = contextAccessor;
        }

        public IActionResult Index(SessionModel model)
        {
            ViewBag.Tests = _context.Tests.Where(x => x.IsActive == true).Select(x => new { x.TestID, x.Name }).ToList();

            SessionModel _model = null;

            if (_contextAccesor.HttpContext.Session.GetString("SessionModel") == null)
            {
                _model = new SessionModel()
                {
                    TestID = model.TestID,
                    UserName = model.UserName,
                    Email = model.Email,
                    Phone = model.Phone
                };

                _contextAccesor.HttpContext.Session.SetObjectAsJson("SessionModel", _model);
            }
            else
            {
                _model = _contextAccesor.HttpContext.Session.GetObjectFromJson<SessionModel>("SessionModel");
            }

            return View(_model);
        }

        public ActionResult Instruction(SessionModel model)
        {
            if(model != null)
            {
                var test = _context.Tests.Include(u => u.TestQuestions)
                                .Where(x => x.IsActive == true && x.TestID == model.TestID).FirstOrDefault();
                if(test!= null)
                {
                    ViewBag.TestName = test.Name;
                    ViewBag.TestDescription = test.Description;
                    ViewBag.QuestionCount = test.TestQuestions.Count;
                    ViewBag.TestDuration = test.DurationInMinute;
                }
            }

            return View(model);
        }


        public IActionResult Register(SessionModel model)
        {
            if(model != null)
                _contextAccesor.HttpContext.Session.SetObjectAsJson("SessionModel", model);

            if(model == null)
                return RedirectToAction("Index");
            
            if(model == null || string.IsNullOrEmpty(model.UserName) || model.TestID < 1)
            {
                TempData["messanger"] = "Invalid Registration details. Please try again";
                return RedirectToAction("Index");
            }

            //To register d user to d system

            Student _user = _context.Students.Where(x => x.Name.Equals(model.UserName, StringComparison.InvariantCultureIgnoreCase)
            && ((string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(x.Email)) || (x.Email == model.Email))
            && ((string.IsNullOrEmpty(model.Phone) && string.IsNullOrEmpty(x.Phone)) || (x.Phone == model.Phone))).FirstOrDefault();

            if (_user == null)
            {
                _user = new Student()
                {
                    Name = model.UserName,
                    Email = model.Email,
                    Phone = model.Phone,
                    EntryDate = DateTime.Now,
                    AccessLevel = 100
                };

                _context.Students.Add(_user);
                _context.SaveChanges();
            }


            Registration registration = _context.Registrations.Where(x => x.StudentID == _user.StudentID
            && x.TestID == model.TestID).FirstOrDefault();


            
            if(registration != null)
            {
                this._contextAccesor.HttpContext.Session.SetString(SessionKeyName, (registration.Token).ToString());
                this._contextAccesor.HttpContext.Session.SetString(SessionKeyAge, (registration.TokenExpireTime).ToString());
            }
            else
            {
                Test test = _context.Tests.Where(x => x.IsActive && x.TestID == model.TestID).FirstOrDefault();
                if(test != null)
                {
                    Registration newRegistration = new Registration()
                    { 
                        RegistrationDate = DateTime.Now,
                        TestID = model.TestID,
                        Token = Guid.NewGuid(),
                        StudentID = _user.StudentID,
                        TokenExpireTime = DateTime.Now.AddMinutes(test.DurationInMinute)
                    };

                    //_user.Registrations.Add(newRegistration);
                    _context.Registrations.Add(newRegistration);
                    _context.SaveChanges();
          
                    this._contextAccesor.HttpContext.Session.SetString(SessionKeyName, (newRegistration.Token).ToString());
                    this._contextAccesor.HttpContext.Session.SetString(SessionKeyAge, (newRegistration.TokenExpireTime).ToString());                    
                }
            }
            
            return RedirectToAction("EvalPage", new { @token = _contextAccesor.HttpContext.Session.GetString(SessionKeyName)});
        }


        public ActionResult EvalPage(Guid token, int? qno)
        {
            if(token == null)
            {
                TempData["message"] = "You have an invalid token. Please re-register and try again";
                return RedirectToAction("Index");
            }

            var registration = _context.Registrations.Where(x => x.Token.Equals(token)).FirstOrDefault();

            if (registration == null)
            {
                TempData["message"] = "This token is invalid";
                return RedirectToAction("Index");
            }

            if (registration.TokenExpireTime < DateTime.Now)
            {
                TempData["message"] = "This exam duration has expired at " + registration.TokenExpireTime.ToString();
                return RedirectToAction("Index");
            }

            if (qno.GetValueOrDefault() < 1)
                qno = 1;

            var testQuestionId = _context.TestQuestions
                .Where(x => x.TestID == registration.TestID && x.QuestionNumber == qno)
                .Select(x => x.TestQuestionID).FirstOrDefault();

            if(testQuestionId > 0)
            {
                var _model = _context.TestQuestions.Where(x => x.TestQuestionID == testQuestionId)
                                .Select(x => new QuestionModel()
                                {
                                    QuestionType = x.Questions.QuestionType,
                                    QuestionNumber = x.QuestionNumber,
                                    Question = x.Questions.Question1,
                                    Point = x.Questions.Points,
                                    TestId = x.TestID,
                                    Token = registration.Token,
                                    TestName = x.Tests.Name,
                                    Options = x.Questions.Choices.Where(y => y.IsActive == true).Select(y => new QXModel()
                                    {
                                        ChoiceId = y.ChoiceID,
                                        Label = y.Label,

                                    }).ToList()
                                }).FirstOrDefault();

                var savedAnswers = _context.TestPapers.Where(x => x.TestQuestionID == testQuestionId && x.RegistrationID == registration.RegistrationID && x.Choices.IsActive == true)
                                    .Select(x => new { x.ChoiceID, x.Answer }).ToList();

                foreach(var savedAnswer in savedAnswers)
                {
                    _model.Options.Where(x => x.ChoiceId == savedAnswer.ChoiceID).FirstOrDefault().Answer = savedAnswer.Answer;
                }

                _model.TotalQuestionInset = _context.TestQuestions.Where(x => x.Questions.IsActive == true && x.TestID == registration.TestID).Count();

                ViewBag.TimeExpire = registration.TokenExpireTime;

                return View(_model);
            }
            else
            {
                return View("Error");
            }

        }


        [HttpPost]
        public ActionResult PostAnswer(AnswerModel choices)
        {
            var registration = _context.Registrations.Where(x => x.Token.Equals(choices.Token)).FirstOrDefault();

            if (registration == null)
            {
                TempData["message"] = "This token is invalid";
                return RedirectToAction("Index");
            }
            if (registration.TokenExpireTime < DateTime.Now)
            {
                TempData["message"] = "The exam duration has expired at " + registration.TokenExpireTime.ToString();
                return RedirectToAction("Index");
            }

            var testQuestionInfo = _context.TestQuestions.Where(x => x.TestID == registration.TestID
                && x.QuestionNumber == choices.QuestionId)
                 .Select(x => new
                 {
                     TQId = x.TestQuestionID,
                     QT = x.Questions.QuestionType,
                     QID = x.TestQuestionID,
                     POINT = (decimal)x.Questions.Points
                 }).FirstOrDefault();

            if (testQuestionInfo != null)
            {
                if (choices.UserChoices.Count > 1)
                {
                    //var allPointvalueOfChoices =
                    //    (
                    //        from a in _context.Choices.Where(x => x.IsActive)
                    //        join b in choices.UserSelectedId on a.ChoiceID equals b
                    //        select new { a.ChoiceID, Points = (decimal)a.Points }).AsEnumerable()
                    //        .Select(x => new TestPaper()
                    //        {
                    //            RegistrationID = registration.RegistrationID,
                    //            TestQuestionID = testQuestionInfo.QID,
                    //            ChoiceID = x.ChoiceID,
                    //            Answer = "CHECKED",
                    //            MarkScored = Math.Floor((testQuestionInfo.POINT / 100.00M) * x.Points)

                    //        }

                    //    ).ToList();
                }

                else
                {
                    _context.TestPapers.Add(new TestPaper()
                    {
                        RegistrationID = registration.RegistrationID,
                        TestQuestionID = testQuestionInfo.QID,
                        ChoiceID = choices.UserChoices.FirstOrDefault().ChoiceId,
                        MarkScored = 1,
                        Answer = choices.Answer
                    });
                }
            }

            return View();
        }





        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
