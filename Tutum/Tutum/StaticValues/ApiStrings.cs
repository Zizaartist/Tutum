using System;
using System.Collections.Generic;
using System.Text;

namespace Tutum.StaticValues
{
    public class ApiStrings
    {
        public const string HOST = "https://tutumapi.azurewebsites.net/";
        public const string BLOB = "https://tutumstorage.blob.core.windows.net/images/";
        public const string ENDPOINT = "https://tutumvideoservices-jpwe.streaming.media.azure.net/";
        public const string TEASER_PATH = "https://tutumvideoservices-jpwe.streaming.media.azure.net/8596759d-e57b-4027-aa45-0dec8788ea87/TutumTiser.mp4";

        #region courses

        /// <summary>
        /// GET: api/Courses/
        /// </summary>
        public const string COURSE_CONTROLLER = "api/Courses/";

        #endregion courses

        #region lessons
        
        /// <summary>
        /// GET: api/Lessons/{id}
        /// </summary>
        public const string LESSON_CONTROLLER = "api/Lessons/";
        /// <summary>
        /// GET: api/Lessons/ByCourse/{id}
        /// </summary>
        public const string LESSON_BY_COURSE = "api/Lessons/ByCourse/";

        #endregion lessons

        #region auth

        /// <summary>
        /// POST: api/Auth/Login
        /// </summary>
        public const string AUTH_LOGIN = "api/Auth/Login/";
        /// <summary>
        /// POST: api/Auth/Register/?code={code}
        /// </summary>
        public const string AUTH_REGISTER = "api/Auth/Register/";
        /// <summary>
        /// POST: api/Auth/SmsCheck/?phone={phone}&registrationCheck=True
        /// </summary>
        public const string AUTH_SMS_CHECK = "api/Auth/SmsCheck/";
        /// <summary>
        /// POST: api/Auth/CodeCheck/?code={code}&phone={phone}
        /// </summary>
        public const string AUTH_CODE_CHECK = "api/Auth/CodeCheck/";

        #endregion auth

        #region user

        /// <summary>
        /// GET: api/Users
        /// </summary>
        public const string USERS_CONTROLLER = "api/Users/";
        /// <summary>
        /// PUT: api/Users/ChangeNumber/?newNumber={newNumber}&code={code}
        /// </summary>
        public const string USERS_CHANGE_PHONE = "api/Users/ChangeNumber/";
        /// <summary>
        /// PUT: api/Users/ChangePassword/?newPassword={newPassword}&code={code}
        /// </summary>
        public const string USERS_CHANGE_PASSWORD = "api/Users/ChangePassword/";

        #endregion user
    }
}
