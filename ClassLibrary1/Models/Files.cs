using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Project3.Data.Models
{
    public class Files
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// 标识id
        /// </summary>
        public int fid { get; set; }

        public string filename { get; set; }

        public string filepath { get; set; }

        public string size { get; set; }

        public DateTime createtime { get; set; }

        public int cid { get; set; }
    }
}
