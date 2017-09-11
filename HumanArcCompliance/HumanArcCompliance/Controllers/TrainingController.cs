using AutoMapper;
using HumanArc.Compliance.Shared.Entities;
using HumanArc.Compliance.Shared.Interfaces;
using HumanArc.Compliance.Shared.ViewModels;
using HumanArc.Compliance.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace HumanArc.Compliance.Web.Controllers
{
    [RoutePrefix("Training")]
    [Route("{action=index}")]
    public class TrainingController : Controller
    {
        private readonly ITrainingService _service;
        private readonly IQuestionService _questionService;
        const string UPLOAD_DIR = @"C:\Temp\ComplianceUploads\";


        public TrainingController(ITrainingService service, IQuestionService questionService)
        {
            _service = service;
            _questionService = questionService;
        }

        [Route("Edit/{id}")]
        [Route("")]
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Blank");
            }
            return View("_Blank");
        }

        [Route("Save")]
        [HttpPost]
        public ActionResult Save(TrainingViewModel model)
        {
            var response = _service.Save(model);
            return JsonResponse.Success(response);
        }

        [Route("GetAll")]
        public ActionResult GetAll()
        {
            var response = _service.GetAll();
            return JsonResponse.Success(response);
        }

        [Route("GetById/{id}")]
        public ActionResult GetById(int id)
        {
            var response = _service.GetById(id);
            return JsonResponse.Success(response);
        }

        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var response = _service.Delete(id);
            return JsonResponse.Success(response);
        }

        [Route("AttachFile/{id}")]
        public ActionResult AttachFile(int id, HttpPostedFileBase file)
        {
            var uploadedFileName = file.FileName.ToLower();
            string fileName = null;

            if (file != null && file.ContentLength > 0)
            {
                if (!Directory.Exists(UPLOAD_DIR))
                    Directory.CreateDirectory(UPLOAD_DIR);

                fileName = $"{GetTimestampString()}-{uploadedFileName}";
                file.SaveAs(Path.Combine(UPLOAD_DIR, fileName));
            }

            var response = _service.SetMediaFileName(id, fileName);
            return JsonResponse.Success(response);
        }

        [Route("GetTrainingMedia/{id}")]
        public ActionResult GetTrainingMedia(int id)
        {
            var response = _service.GetById(id);
            byte[] buff = new byte[0];

            if (!string.IsNullOrEmpty(response.FilePathToMedia))
            {
                var fileName = Path.Combine(UPLOAD_DIR, response.FilePathToMedia);
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var br = new BinaryReader(fs);
                    long numBytes = new FileInfo(fileName).Length;
                    buff = br.ReadBytes((int)numBytes);
                }
            }
            return File(buff, "application/pdf", "training.pdf");
        }

        [Route("GetQuestions/{id}")]
        public ActionResult GetQuestions(int id)
        {
            var response = _questionService.GetQuestionsByTrainingId(id);
            return JsonResponse.Success(response);
        }

        [Route("GetQuestionById/{id}")]
        public ActionResult GetQuestionById(int id)
        {
            var response = _questionService.GetById(id);
            return JsonResponse.Success(response);
        }

        [Route("SaveQuestion")]
        public ActionResult SaveQuestion(QuestionViewModel model)
        {
            var response = _questionService.Save(model);
            return JsonResponse.Success(response);
        }

        //[Route("DeleteTraining/{id}")]
        //public ActionResult DeleteTraining(int id)
        //{
        //    _service.SoftDeleteTraining(id);
        //    return JsonResponse.Success("Successfully Deleted");
        //}

        [Route("DeleteQuestion/{trainingId}/{questionId}")]
        public ActionResult DeleteQuestion(int trainingId, int questionId)
        {
            _questionService.Delete(questionId);
            var response = _questionService.GetQuestionsByTrainingId(trainingId);
            return JsonResponse.Success(response);
        }


        //[Route("AddGroup")]
        //public ActionResult AddGroup(ADGroupViewModel model)
        //{
        //    var response = new ADGroupViewModel();

        //    //if (model.ID != 0)
        //    //{
        //    //    response = _service.UpdateGroup(model);
        //    //    return JsonResponse.Success(response);
        //    //}

        //    response = _service.AddGroup(model);
        //    return JsonResponse.Success(response);
        //}

        //[Route("DeleteGroup")]
        //public ActionResult DeleteGroup(ADGroupViewModel model)
        //{
        //    _service.DeleteGroup(model);
        //    return JsonResponse.Success("Item was Removed From Training Successfully");
        //}

        //[Route("GetScheduleById/{id}")]
        //public ActionResult GetScheduleById(int id)
        //{
        //    var response = _scheduleService.GetById(id);
        //    return JsonResponse.Success(_mapper.Map<Schedule, ScheduleViewModel>(response));
        //}

        public JsonResult SearchForUsers(string term)
        {
            var groupsList = ActiveDirectoryHelper.GetADGroups();
            term = term.ToLower();

            Dictionary<string, string> groups = groupsList
                .Where(group => group.Key.ToLower().StartsWith(term))
                .ToDictionary(group => group.Key, group => group.Key);

            if (groups.Count == 0) groups.Add(string.Empty, "(No Results Found)");
            var data = groups.Select(x => new { id = x.Key, value = x.Value });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public static string GetTimestampString()
        {
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString().PadLeft(2, '0');
            string day = DateTime.Now.Day.ToString().PadLeft(2, '0');
            string hour = DateTime.Now.Hour.ToString().PadLeft(2, '0');
            string minute = DateTime.Now.Minute.ToString().PadLeft(2, '0');
            string second = DateTime.Now.Second.ToString().PadLeft(2, '0');
            string miliSec = DateTime.Now.Millisecond.ToString();
            return (string.Format("{0}{1}{2}{3}{4}{5}{6}", year, month, day, hour, minute, second, miliSec));
        }
    }
}
