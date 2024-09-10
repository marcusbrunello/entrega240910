using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaBank.Model.Base;

public enum EnumTriesToBlock : int
{
    BLOCKED = 0,
    MAXATTEMPTSLEFT = 4,
    THREEATTEMPTSLEFT = 3,
    TWOATTEMPTSLEFT = 2,
    ONEATTEMPTSLEFT = 1,
    DEFAULTINIT = 4
}
