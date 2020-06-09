//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore.Validation;

//namespace DbTest.Extension
//{
//    public static class DbEntityValidationResultExtensions
//    {
//        /// <summary>
//        /// Appends dbEntityValidationResult2 <see cref="DbEntityValidationResult.ValidationErrors"/> property to the caller's prop.
//        /// </summary>
//        /// <param name="dbEntityValidationResult1"></param>
//        /// <param name="dbEntityValidationResult2"></param>
//        public static void AppendWith(this DbEntityValidationResult dbEntityValidationResult1, DbEntityValidationResult dbEntityValidationResult2)
//        {
//            if (dbEntityValidationResult1.Entry.Entity != dbEntityValidationResult2.Entry.Entity)
//            {
//                throw new Exception("Trying to append ValidationErrors with different db Entries!");
//            }

//            foreach (var error in dbEntityValidationResult2.ValidationErrors)
//            {
//                dbEntityValidationResult1.ValidationErrors.Add(error);
//            }
//        }

//        public static void AppendWith(this DbEntityValidationResult dbEntityValidationResult1, IEnumerable<DbEntityValidationResult> dbEntityValidationResult2)
//        {
//            foreach (var dbValitaion in dbEntityValidationResult2)
//            {
//                if (dbEntityValidationResult1.Entry.Entity != dbValitaion.Entry.Entity)
//                {
//                    throw new Exception("Trying to append ValidationErrors with different db Entries!");
//                }

//                foreach (var error in dbValitaion.ValidationErrors)
//                {
//                    dbEntityValidationResult1.ValidationErrors.Add(error);
//                }
//            }
//        }
//    }
//}
