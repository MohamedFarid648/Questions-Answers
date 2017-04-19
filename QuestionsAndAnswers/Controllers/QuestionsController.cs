using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuestionsAndAnswers.Models;

namespace QuestionsAndAnswers.Controllers
{
   // [Authorize]
    public class QuestionsController : Controller
    {
        private DBContextClass db = new DBContextClass();

        // GET: Questions
        public ActionResult Index()
        {
            if (Session["UserSessionEmail"] != null)
            {
                return View(db.Questions.ToList());

            }
            else
            {
                return RedirectToAction("Login", "Users");
            }

        }


        public PartialViewResult AddQuestion() {

            Question question = new Question();
            if (Session["UserSessionEmail"] != null)
            {
                string email = Session["UserSessionEmail"].ToString();
                int UserID= QuestionsAndAnswers.Models.User.returnUserID(email);
                string text = Request.Form.Get("Text");
                question.UserID = UserID;
                question.text = text;
                db.Questions.Add(question);
                db.SaveChanges();
                return PartialView("ViewQuestionUsingAjax", question);
            }

            else
            {
                return PartialView("ViewQuestionUsingAjax", question);
            }

        }

        public PartialViewResult AddAnswer() {

            Answer answer = new Answer();
            if (Session["UserSessionEmail"] != null)
            {
                string email = Session["UserSessionEmail"].ToString();
                int UserID = QuestionsAndAnswers.Models.User.returnUserID(email);
                string text = Request.Form.Get("Text");
                int questionID =Convert.ToInt32( Request.Form.Get("QuestionID"));
                answer.UserID = UserID;
                answer.text = text;
                answer.QuestionID = questionID;
                db.Answers.Add(answer);
                db.SaveChanges();
                return PartialView("ViewAnswerUsingAjax", answer);
            }

            else
            {
                return PartialView("ViewAnswerUsingAjax", answer);
            }

        }



        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["UserSessionEmail"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Question question = db.Questions.Find(id);
                if (question == null)
                {
                    return HttpNotFound();
                }
                return View(question);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
           
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserSessionEmail"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Question question = db.Questions.Find(id);
                if (question == null)
                {
                    return HttpNotFound();
                }
                return View(question);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }


        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,text")] Question question)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                if (Session["UserSessionEmail"] != null)
                {


                    string user_email = Session["UserSessionEmail"].ToString();
                    user = db.Users.Single(u => u.Email == user_email);
                    question.UserID = user.ID;
                    db.Entry(question).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login", "Users");
                }

            }
            return View(question);
        }
      
       // can't create constraint on delete cascade on update cascade on Answers table
        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserSessionEmail"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Question question = db.Questions.Find(id);
                if (question == null)
                {
                    return HttpNotFound();
                }
                return View(question);
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }

        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserSessionEmail"] != null)
            {
                Question question = db.Questions.Find(id);
                db.Questions.Remove(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }


        }
       
        /*   public PartialViewResult ReturnAnswers(int questionID) {

               List<Answer> answers = db.Answers.Where(a => a.QuestionID == questionID).ToList();
               return PartialView(answers);

           }*/
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*
        // GET: Questions/Create
        public ActionResult Create()
        {
            if (Session["UserSessionEmail"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,text")] Question question)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                if (Session["UserSessionEmail"] != null)
                {


                    string user_email = Session["UserSessionEmail"].ToString();
                    user = db.Users.Single(u => u.Email == user_email);
                    question.UserID =user.ID;
                    db.Questions.Add(question);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login", "Users");
                }

            }

            return View(question);
        }

        // GET: Questions/CreateAnswer
        public ActionResult CreateAnswer()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnswer([Bind(Include = "id,text,QuestionID")] Answer answer)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                if (Session["UserSessionEmail"] != null)
                {

                    int QuestionId =Convert.ToInt32( Request.Form.Get("QuestionID").ToString());
                    string user_email = Session["UserSessionEmail"].ToString();
                    user = db.Users.Single(u => u.Email == user_email);
                    answer.UserID = user.ID;
                
                   db.Answers.Add(answer);
                   db.SaveChanges();
                    return RedirectToAction("Details", "Questions", new { id = answer.QuestionID });
                }
                else
                {
                    return RedirectToAction("Login", "Users");
                }

            }
            return View(answer);

        }

        */
    }
}
