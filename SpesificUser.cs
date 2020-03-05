using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
 
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using DL.Model;
using BE;
namespace UI
{
     public  class  SpesificUser
    {
      public static DbEntityEntry<BE.User> ThisEntry { get; set; }
  
    }
}
