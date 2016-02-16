using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
   public class ScreenPermission
    {
        private int screenCode;
        private int actionCode;
        private String descriptionAction;

        public String DescriptionAction
        {
            get { return descriptionAction; }
            set { descriptionAction = value; }
        }


        public int ActionCode
        {
            get { return actionCode; }
            set { actionCode = value; }
        }

        public int ScreenCode
        {
            get { return screenCode; }
            set { screenCode = value; }
        }
    }
}
