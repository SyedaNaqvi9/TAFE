using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAFE
{
    class clsValidation
    {
        
        public bool ValidateForEmptiness(String str)
        {
            if (str == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateForLength(String str, int len)
        {
            if (str.Length != len)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ValidateForNumeric(String str)
        {
            try
            {
                int num = int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //public bool ValidateCombobox(ComboBox cmb)
        //{
        //    if (cmb.SelectedIndex == "")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

    }
}
