//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HumanArcCompliance
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserQuizQuestionAnswer
    {
        public int id { get; set; }
        public int quizId { get; set; }
        public int questionId { get; set; }
        public int answerId { get; set; }
        public Nullable<bool> isChecked { get; set; }
        public Nullable<bool> isApproved { get; set; }
    
        public virtual Answer Answer { get; set; }
        public virtual Question Question { get; set; }
        public virtual Quize Quize { get; set; }
    }
}
