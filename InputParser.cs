using Newtonsoft;
using System;
using System.IO;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;

namespace CVtesting;

public class InputParser {
    public const string testpath = "/home/roni/repos/CVtesting/test.docx"; 
    
    public static List<String> ExtractTextFromDocx(string filePath)
    {
        using WordprocessingDocument docReader = WordprocessingDocument.Open(filePath, false);  // open read-only

        string header = string.Join("\n", docReader.MainDocumentPart!.HeaderParts.Select(h => h.Header.InnerText));
        string footer = string.Join("\n", docReader.MainDocumentPart.FooterParts.Select(f => f.Footer.InnerText));
        var paragraphs = docReader.MainDocumentPart.Document.Body!.Elements<Paragraph>();

        List<String> segments = [];
        segments.Add(header);
        foreach (var paragraph in paragraphs) {
            string text = paragraph.InnerText;
            segments.Add(text);
        }
        segments.Add(footer);
        string.Join("\n", segments);
        
        return segments;
    }
}

