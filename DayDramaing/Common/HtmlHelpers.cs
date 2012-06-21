using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;

namespace DayDramaing.Common
{
    public static class HtmlHelpers
    { 
        public static MvcHtmlString GenerateCaptcha(this HtmlHelper helper )
        {
	        var captchaControl = new Recaptcha.RecaptchaControl
        	    {
                    ID = "recaptcha",
                    Theme = "clean",
                    PublicKey = "6LcVHtMSAAAAAMfZ5RqWbu5ukfECkdnJvNUYU41e",
                    PrivateKey = "6LcVHtMSAAAAAO_jK7KyXGWsoj7hRFkYuYlnoEQU"
		        };

	        var htmlWriter = new HtmlTextWriter( new StringWriter() );

	        captchaControl.RenderControl(htmlWriter);

            return new MvcHtmlString(htmlWriter.InnerWriter.ToString());
        }

    }
}