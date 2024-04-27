using BSCare.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BSCare.Data
{
    public static class GlobalVar
    {
        public static List<SelectListItem> SelectList = new List<SelectListItem> { new SelectListItem { Text = "עובד רשות מקומית", Value = "0" } };
        public static List<SelectListItem> EditInitiatorList = new List<SelectListItem> { new SelectListItem { Text = "עובד רשות מקומית", Value = "0" }, new SelectListItem { Text = "אפליקציית סלולר", Value = "1" } };
        public static List<SelectListItem> StatusSelectList = new List<SelectListItem> { new SelectListItem { Text = "חדש במערכת", Value = "0" } };
        public static List<SelectListItem> HazardSelectList = new List<SelectListItem> { new SelectListItem { Text = "תקלה מכנית", Value = "0" }, new SelectListItem { Text = "תקלת תאורה", Value = "1" }, new SelectListItem { Text = "תקלת ניקיון", Value = "2" }, new SelectListItem { Text = "תקלה בלוח הדיגיטלי", Value = "3" }, new SelectListItem { Text = "אחר", Value = "4" } };
        /*public static List<SelectListItem> EditStatusSelectList = new List<SelectListItem> { new SelectListItem { Text = "חדש במערכת", Value = "0" }, new SelectListItem { Text = "עבר לטיפול", Value = "1" }, new SelectListItem { Text = "טופל \\ נסגר", Value = "2" } };*/
        public static List<SelectListItem> EditStatusSelectList = new List<SelectListItem> { new SelectListItem { Text = "חדש במערכת", Value = "0" }, new SelectListItem { Text = "עבר לטיפול", Value = "1" }};

        /*        public static List<SelectListItem> SelectList = new List<SelectListItem> { new SelectListItem { Text = "עובד רשות מקומית", Value = "0" }, new SelectListItem { Text = "אפליקציית סלולר", Value = "1" } };
                public static List<SelectListItem> StatusSelectList = new List<SelectListItem> { new SelectListItem { Text = "חדש במערכת", Value = "0" }, new SelectListItem { Text = "עבר לטיפול", Value = "1" }, new SelectListItem { Text = "טופל \\ נסגר", Value = "2" } };*/

        public static string? city { get; set; }
        public static  List<Stop> stops_in_city { get; set; }
    }
}
