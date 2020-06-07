using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;

namespace serverChallengeMe.Models
{
    public class Institution
    {
        public string InstitutionID { get; set; }
        public string InstitutionName { get; set; }
        public string EducationStages { get; set; }
        public string Phone { get; set; }

        public Institution() { }

        public Institution(string institutionID, string institutionName, string educationStages, string phone)
        {
            InstitutionID = institutionID;
            InstitutionName = institutionName;
            EducationStages = educationStages;
            Phone = phone;
        }

        public DataTable getInstitutions()
        {
            DBservices dBservices = new DBservices();
            return dBservices.getInstitutions();
        }
    }
}