using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpgradePackTool.Model
{
    public class ProductModel
    {
        public ProductModel(string productLineCode, string productLine, string productType, List<ProductDetailedModel> productDetailedModel)
        {
            this.ProductLineCode = productLineCode;
            this.ProductLine = productLine;
            this.ProductType = productType;
            this.ProductDetailedModel = productDetailedModel;
        }
        //产品线编码
        public string ProductLineCode { get; set; }

        //产品线
        public string ProductLine { get; set; }

        //产品类型
        public string ProductType { get; set; }

        public List<ProductDetailedModel> ProductDetailedModel { get; set; }

    }

    public class ProductDetailedModel
    {
        public ProductDetailedModel (string productModelCode, string productModel)
        {
            ProductModelCode = productModelCode;
            ProductModel = productModel;
        }
        //产品线编码
        public string ProductModelCode { get; set; }
        //产品线编码
        public string ProductModel { get; set; }
    }

    public class ProductModelList
    {
        public ProductModelList(List<ProductModel> productModel)
        {
            ProductModel = productModel;
        }

        public List<ProductModel> ProductModel { get; set; }
    }
}
