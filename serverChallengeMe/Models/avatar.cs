using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using serverChallengeMe.Models.DAL;

namespace serverChallengeMe.Models
{
    public class Avatar
    {
        public int AvatarID { get; set; }
        public string AvatarName { get; set; }
        public string AvatarLev1 { get; set; }
        public string AvatarLev2 { get; set; }
        public string AvatarLev3 { get; set; }
        public string AvatarLev4 { get; set; }
        public string AvatarLev5 { get; set; }
        public string AvatarLev6 { get; set; }

        public Avatar() { }

        public Avatar(int avatarID, string avatarName, string avatarLev1, string avatarLev2, string avatarLev3, string avatarLev4, string avatarLev5, string avatarLev6)
        {
            AvatarID = avatarID;
            AvatarName = avatarName;
            AvatarLev1 = avatarLev1;
            AvatarLev2 = avatarLev2;
            AvatarLev3 = avatarLev3;
            AvatarLev4 = avatarLev4;
            AvatarLev5 = avatarLev5;
            AvatarLev6 = avatarLev6;
        }

        //public DataTable getAvatar()
        //{
        //    DBservices dBservices = new DBservices();
        //    return dBservices.getAvatar();
        //}

        //public int postAvatar(Avatar avatar)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.postAvatar(avatar);
        //}

        //public int deleteAvatar(int id)
        //{
        //    DBservices dbs = new DBservices();
        //    return dbs.deleteAvatar(id);
        //}
    }
}