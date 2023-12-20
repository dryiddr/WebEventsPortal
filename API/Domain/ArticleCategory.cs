namespace Domain;

using System;
using System.Collections.Generic;

public class ArticleCategory : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}