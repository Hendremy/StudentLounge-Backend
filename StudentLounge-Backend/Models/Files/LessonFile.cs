﻿namespace StudentLounge_Backend.Models.Files
{
    public class LessonFile
    {
        public AppUser Author { get; set; }
        public string FileName { get; set; }
        public DateTime AddedOn { get; set; }
        public string FilePath { get; set; }
        public LessonFileType Type { get; set; }
        public Lesson Lesson { get; set; }
    }

    public enum LessonFileType
    {
        summary, notes
    }
}