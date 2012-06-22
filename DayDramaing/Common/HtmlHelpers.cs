using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;
using System.Configuration;

namespace DayDramaing.Common
{
    public static class HtmlHelpers
    { 
        public static MvcHtmlString GenerateCaptcha(this HtmlHelper helper )
        {

            var pubicKey = ConfigurationManager.AppSettings["RecaptchaPublic"];
            var privateKey = ConfigurationManager.AppSettings["RecaptchaPrivate"];

	        var captchaControl = new Recaptcha.RecaptchaControl
        	    {
                    ID = "recaptcha",
                    Theme = "clean",
                    PublicKey = pubicKey,
                    PrivateKey = privateKey
		        };

	        var htmlWriter = new HtmlTextWriter( new StringWriter() );

	        captchaControl.RenderControl(htmlWriter);

            return new MvcHtmlString(htmlWriter.InnerWriter.ToString());
        }

    }
}