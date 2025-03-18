using Newtonsoft;
using System;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;

namespace CVtesting;

public class InputParser {
    
    public static List<string> ExtractTextFromDocx(string filePath, ref string text)
    {
        using WordprocessingDocument docReader = WordprocessingDocument.Open(filePath, false);  // open read-only

        var paragraphs = docReader.MainDocumentPart!.Document.Body!.Elements<Paragraph>();

        List<string> segments = [];
        foreach (var paragraph in paragraphs) {
            string innerText = paragraph.InnerText;
            segments.Add(innerText);
        }

        
        if (text.Length < 50) {  // failsafe since cv templates are sometimes formatted in tables
            foreach (var table in docReader.MainDocumentPart!.Document.Body.Elements<Table>())
            {
                foreach (var row in table.Elements<TableRow>())
                {
                    foreach (var cell in row.Elements<TableCell>())
                    {
                        var cellLines = cell.Elements<Paragraph>() // ignore empty formatting cells
                            .Select(p => p.InnerText.Trim())       // and join with spaces to prevent merged words
                            .Where(line => !string.IsNullOrEmpty(line));
                        string cellText = string.Join(" ", cellLines);  

                        if (string.IsNullOrWhiteSpace(cellText)) { segments.Add(" "); }
                        else { segments.Add(cellText); }
                    }
                }
            }
            
        }
        text = string.Join("|", segments);
        if (text.Length < 50) {
            throw new Exception("Unable to parse resume, not all functions will work (TODO)");
        }
        return segments;
    }
}

