﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PHARMA_Z;
using PHARMA_Z.DAL.Database;
using PHARMA_Z.Model;
using System.Data.SqlClient;
using System.Data;

namespace PHARMA_Z.DAL
{
    public class BrandService
    {
        private DbClient _dbClient = null;
        public BrandService()
        {
            _dbClient = DbClient.CreateDbClient();
        }
        public List<string> GetBrandNames()
        {
            List<string> Brandslist = new List<string>();
            SqlCommand command = this._dbClient.CreateSqlCommand("SELECT Name FROM Brand", null, CommandType.Text);
            DataTable dtBrand = _dbClient.GetDataTable(command);
            if (dtBrand != null && dtBrand.Rows.Count > 0)
            {
                for (int i = 0; i < dtBrand.Rows.Count; i++)
                {
                    Brandslist.Add(dtBrand.Rows[i].Field<string>("Name"));
                }
            }
            return Brandslist;
        }
        public int GetBrandId (Brand brand)
        {
            SqlCommand command = this._dbClient.CreateSqlCommand("SELECT Id FROM Brand WHERE Name = '" + brand.BrandName + "'", null, CommandType.Text);
            DataTable dtBrand = _dbClient.GetDataTable(command);
            if (dtBrand != null && dtBrand.Rows.Count > 0)
            {
                for (int i = 0; i < dtBrand.Rows.Count; i++)
                {
                    brand.BrandId = dtBrand.Rows[i].Field<int>("Id");
                }
            }
            else
            {
                brand.BrandId = 0;
            }
            return brand.BrandId;
        }
        public DataTable GetBrand (int BrandId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter()
            {
                ParameterName = "@id",
                Value = BrandId
            });
            SqlCommand command = this._dbClient.CreateSqlCommand("GetBrand",parameters);
            DataTable dtBrand = _dbClient.GetDataTable(command);
            return dtBrand;
        }
    }
}
