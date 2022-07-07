using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    class Manager
    {
        internal object GetGender()
        {
            var obj = new SANITARIUMEntities();
            var q = from gen in obj.GenderInfoes select gen;
            return q.ToList();
        }

        internal object GetAgeStatus()
        {
            var obj = new SANITARIUMEntities();
            var q = from at in obj.AgeStatusInfoes select at;
            return q.ToList();
        }

        internal object GetDiscountAuthor()
        {
            var obj = new SANITARIUMEntities();
            var q = from da in obj.DiscountAuthorityInfoes select da;
            return q.ToList();
        }

        internal object GetRefer()
        {
            var obj = new SANITARIUMEntities();
            var q = from refer in obj.ReferInfoes select refer;
            return q.ToList();
        }

        internal object GetTestName()
        {
            var obj = new SANITARIUMEntities();
            var q = from testname in obj.DignosticTestDetails select testname;
            return q.ToList();
        }

        internal object GetDoctor()
        {
            var obj = new SANITARIUMEntities();
            var q = from doc in obj.DoctorInfoes select doc;
            return q.ToList();
        }

        public List<SP_Day_Wise_Sale_Summary_2_Result1> GetSaleSummary(DateTime sD, DateTime eD)
        {
            var context = new SANITARIUMEntities();
            var q = context.SP_Day_Wise_Sale_Summary_2(sD,eD);
            return q.ToList();
        }
    }
}
