using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class TaxonomyPostType
    {
        public int Id { get; set; }
        public int TaxonomyId { get; set; }
        public int PostTypeId { get; set; }


        public Taxonomy Taxonomy { get; set; }
        public PostType PostType { get; set; }
    }
}
