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

        #endregion lessons

        #region user



        #endregion user
    }
}
