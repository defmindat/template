using EatWise.Common.Domain;
using Constants = EatWise.Common.Domain.Constants;

namespace EatWise.Recipes;

public class Recipe : Entity
{
    public Guid Id { get; private set; }
    public string Name { get; protected set; }
    public string Text { get; protected set; }

    public DateTime CreatedDate { get; protected set; }
    
    private Recipe()
    {
        Name = Constants.Undefined;
        Text = Constants.Undefined;
    }

    public static Recipe Create(
            Guid id,
            string name,
            string text,
            DateTime createdDate)
    {
        return new Recipe(id, name, text, createdDate); 
    }

    private Recipe(
            Guid id,
            string name,
            string text,
            DateTime createdDate)
    {
        Id = id;
        Name = name;
        Text = text;
        CreatedDate = createdDate;
    }
}
