using Interfaces;
using CSBibleBook = CSBible.Book;

namespace Utils;

public class BiblicalIndex : IBiblicalIndex<CSBibleBook>
{
    public string BookTitle { get; set; }
    public CSBibleBook Book { get; set; }
    public int Chapter { get; set; }
    public int? Verse { get; set; }
    public int? VerseRange { get; set; }

    public BiblicalIndex(string book, int chapter, int? verse = null, int? verseRange = null)
    {
        this.BookTitle = book;
        this.Chapter = chapter;
        this.Verse = verse;
        this.VerseRange = verseRange;

        this.SetCSBibleBook();
    }

    public void SetCSBibleBook()
    {
        string csbibleBookName;

        char firstCharacter = this.BookTitle[0];
        if (char.IsDigit(firstCharacter))
        {
            switch (firstCharacter)
            {
                case '1':
                    csbibleBookName = "First_" + this.BookTitle.Substring(2);
                    break;

                case '2':
                    csbibleBookName = "Second_" + this.BookTitle.Substring(2);
                    break;

                case '3':
                    csbibleBookName = "Third_" + this.BookTitle.Substring(2);
                    break;

                default:
                    throw new ArgumentException("Can't find this book");
            }
        }
        else
        {
            switch (this.BookTitle)
            {
                case "Song Of Solomon":
                case "Song Of Songs":
                    csbibleBookName = "SongofSolomon";
                    break;

                case "Psalm":
                    csbibleBookName = "Psalms";
                    break;

                default:
                    csbibleBookName = this.BookTitle;
                    break;
            }
        }


        try
        {
            this.Book = (CSBibleBook)Enum.Parse(typeof(CSBibleBook), csbibleBookName);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException($"Can't find the book of '{BookTitle}'");
        }
    }

    public override string ToString()
    {
        if (VerseRange is not null)
        {
            return $"{BookTitle} {Chapter}:{Verse}-{VerseRange}";
        }

        if (Verse is null)
        {
            return $"{BookTitle} {Chapter}";
        }

        return $"{BookTitle} {Chapter}:{Verse}";
    }
}