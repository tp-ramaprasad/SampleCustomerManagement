using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.MVC.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
    }

    public class BaseActionModel: BaseModel
    {
        /// <summary>
        /// this property is only used for binding dropdowns.
        /// </summary>
        public GetReferenceInfoModel? ReferenceInfoData { get; set; }
    }
}
