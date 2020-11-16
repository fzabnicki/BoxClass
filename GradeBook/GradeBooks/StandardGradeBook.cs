using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class StandardGradeBook : BaseGradeBook
    {
        public StandardGradeBook(string name, bool weighted) : base(name, weighted)
        {
            Type = GradeBookType.Standard;
            IsWeighted = weighted;
        }
    }
}
