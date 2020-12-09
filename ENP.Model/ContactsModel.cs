using ShivOhm.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENP.Model
{
    [Table(TableName ="Contacts")]
    public class ContactsModel
    {
        [Key]
        public int ID { get; set; }
        public int TenantID { get; set; }       
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int? AccountID { get; set; }
        public int ContactTypeID { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime? AssignedOn { get; set; }
        public int? OwnerID { get; set; }
        public DateTime? OwnerAssignedOn { get; set; }
        public int? BillingAddressID { get; set; }
        public int? ShippingAddressID { get; set; }
        public int? ProspectStageID { get; set; }
        public int? LeadStageID { get; set; }
        public int? CampaignID { get; set; }
        public int? LeadSourceID { get; set; }
        public string RecordKey { get; set; }
        public int? UserID { get; set; }
        public bool IsActive { get; set; }
        public string ProfilePhoto { get; set; }
        public string ExternalSystemKey { get; set; }
        public bool? IsSelfEmployed { get; set; }
        public int? EmployerYears { get; set; }
        public string Employer { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string AssistantName { get; set; }
        public string AssistantPhone { get; set; }
        public int? ReportsToID { get; set; }
        public int? Rating { get; set; }
        public int? Score { get; set; }
        public bool? DoNotCall { get; set; }
        public bool? EmailOptOut { get; set; }
        public bool? IsMarried { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? Modified { get; set; }
        public int? ModifiedBy { get; set; }
        public int? UID { get; set; }
        public string Gender { get; set; }
        public string SSN { get; set; }
        public string ClassName { get; set; }
        public bool IsTobaccoUser { get; set; }
        public string GenderIdentified { get; set; }
        public string PreferredName { get; set; }
        public string MaidenName { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string ShortCode { get; set; }
        public int? ContactPictureID { get; set; }
        public int? StatusID { get; set; }
        public int? StageID { get; set; }        
    }
}
