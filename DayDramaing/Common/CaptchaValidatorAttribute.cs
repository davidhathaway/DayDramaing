using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace DayDramaing.Common
{
    public class CaptchaValidatorAttribute : ActionFilterAttribute
    {
        private const string CHALLENGE_FIELD_KEY = "recaptcha_challenge_field";
        private const string RESPONSE_FIELD_KEY = "recaptcha_response_field";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var privateKey = ConfigurationManager.AppSettings["RecaptchaPrivate"];
            var captchaChallengeValue = filterContext.HttpContext.Request.Form[CHALLENGE_FIELD_KEY];
            var captchaResponseValue = filterContext.HttpContext.Request.Form[RESPONSE_FIELD_KEY];
            var captchaValidtor = new Recaptcha.RecaptchaValidator
                                      {
                                          PrivateKey = privateKey,
                                          RemoteIP = filterContext.HttpContext.Request.UserHostAddress,
                                          Challenge = captchaChallengeValue,
                                          Response = captchaResponseValue
                                      };

            var recaptchaResponse = captchaValidtor.Validate();

	        // this will push the result value into a parameter in our Action
            filterContext.ActionParameters["CaptchaValid"] = recaptchaResponse.IsValid;

            if (!recaptchaResponse.IsValid && filterContext.Controller is Controller) 
            {
                var controller = filterContext.Controller as Controller;
                controller.ModelState.AddModelError("CaptchaValid", "Captcha wasn't recognised please try again.");
            }

            base.OnActionExecuting(filterContext);
        }
    }

}