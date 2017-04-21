using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleManager.BusinessLogic.Services
{
    public abstract class AbstractExporterService
    {
        /// <summary>
        /// Define the styles used in a document
        /// </summary>
        /// <param name="doc">The document to define styles for</param>
        protected void DefineStyles(Document doc)
        {
            Style style = doc.Styles.AddStyle("TOC", "Normal");
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
            style.ParagraphFormat.Font.Color = Colors.Black;

            Style normal = doc.Styles["Normal"];
            normal.Font.Name = "Calibri";
            normal.Font.Size = 11;

            Style h1 = doc.Styles["Heading1"];
            h1.Font.Name = "Arial";
            h1.Font.Size = 16;
            h1.Font.Bold = true;

            Style h2 = doc.Styles["Heading2"];
            h2.Font.Name = "Calibri";
            h2.Font.Size = 14;
            h2.Font.Bold = true;
            h2.Font.Italic = true;

            Style err = doc.AddStyle("error", "Normal");
            err.Font.Name = "Arial";
            err.Font.Color = Colors.Red;

            Style list = doc.AddStyle("bulletlist", "Normal");
            list.ParagraphFormat.LeftIndent = new Unit(0.5, UnitType.Centimeter);
            list.ParagraphFormat.ListInfo.ListType = ListType.BulletList1;

            Style list2 = doc.AddStyle("bulletlist2", "Normal");
            list2.ParagraphFormat.LeftIndent = new Unit(1.5, UnitType.Centimeter);
            list2.ParagraphFormat.ListInfo.ListType = ListType.BulletList2;
        }

        /// <summary>
        /// Builds the cover of the document
        /// </summary>
        /// <param name="doc">The document to build a cover for</param>
        /// <param name="coverTitle">The cover title</param>
        protected void BuildCover(Document doc, string coverTitle)
        {
            Section sect = doc.AddSection();

            //this MIGHT cause trouble later...
            /*
            string tempImg = System.Environment.CurrentDirectory + "\\tempAvansLogo.jpg";

            Resources.Avans_Logo.Save(tempImg, ImageFormat.Jpeg);
            Image img = sect.AddImage(tempImg);
            img.WrapFormat.Style = WrapStyle.Through;
            img.Left = ShapePosition.Right;
            img.Top = ShapePosition.Bottom;
             */

            Paragraph p = sect.AddParagraph("\n\nAvans Hogeschool\n" + coverTitle + "\n\nDatum: " + DateTime.Now.Date.ToString("d-MM-yyyy"));
            p.Format.Font = new Font("Arial", "12");
        }
    }
}
