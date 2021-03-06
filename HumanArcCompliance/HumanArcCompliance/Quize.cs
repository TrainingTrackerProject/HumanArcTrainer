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
    
    public partial class Quize
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quize()
        {
            this.Questions = new HashSet<Question>();
            this.UserQuizQuestionAnswers = new HashSet<UserQuizQuestionAnswer>();
        }
    
        public int id { get; set; }
        public int groupId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string media { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime preferDate { get; set; }
        public System.DateTime expiredDate { get; set; }
    
        public virtual Group Group { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserQuizQuestionAnswer> UserQuizQuestionAnswers { get; set; }
    }
}
