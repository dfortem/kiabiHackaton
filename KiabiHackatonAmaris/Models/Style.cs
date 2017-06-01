using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiabiHackatonAmaris.Models
{
    public class Style
    {
        public string StyleId { get; set; }
        public string StyleId15 { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string Description { get; set; }
        public string BrandId { get; set; }
        public string BrandLabel { get; set; }
        public string SizeGrid { get; set; }
        public string Type { get; set; }
        public List<StyleCollection> StyleCollections { get; set; }
        public List<Color> Colors { get; set; }
        //public List<Size> sizes { get; set; }
        //public List<Element> elements { get; set; }

        public Position GetPosition(string colorId)
        {
            return new Position
            {
                GroupId = StyleCollections.FirstOrDefault()?.GroupId,
                SubCategoryId = Colors.FirstOrDefault(color => color.ColorId == colorId)?
                                .ColorCollections.FirstOrDefault()?.SubCategoryId
            };
        }
    }

    public class StyleCollection
    {
        public string CollectionId { get; set; }
        public string Description { get; set; }
        public string ProcessId { get; set; }
        public string ProcessLabel { get; set; }
        public string CategoryId { get; set; }
        public string CategoryLabel { get; set; }
        public string GroupId { get; set; }
        public string GroupLetter { get; set; }
        public string MarketId { get; set; }
        public string DepartmentId { get; set; }
        public string ClassId { get; set; }
        public string LayerTypeId { get; set; }
        public string LayerTypeLabel { get; set; }
    }

    public class Color
    {
        public string ColorId { get; set; }
        public string ColorOrder { get; set; }
        public string Pantone { get; set; }
        public List<int> Rgb { get; set; }
        public string Picture { get; set; }
        public string SpecificityCode { get; set; }
        public string BaseColorId { get; set; }
        public string BaseColorLabel { get; set; }
        public List<ColorCollection> ColorCollections { get; set; }
    }

    public class ColorCollection
    {
        public string CollectionId { get; set; }
        public string SubCategoryId { get; set; }
        public string SubCategoryLabel { get; set; }
        public string StartPeriodId { get; set; }
        public string EndPeriodId { get; set; }
        public string TypologyId { get; set; }
        public string TypologyLabel { get; set; }
        public string GlobalAssortementRangeCode { get; set; }
        //public AssortementRangeCodeByChannels assortementRangeCodeByChannels { get; set; }
    }
}