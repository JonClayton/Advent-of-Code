namespace AdventOfCode.Solutions2015;

public class Solution2015Dec15 : Solution<long?>
{
    protected override long? FirstSolution(List<string> lines) => GeneralSolution(lines, true);

    protected override long? SecondSolution(List<string> lines) => GeneralSolution(lines, false);

    private static long GeneralSolution(List<string> lines, bool isFirstSolution)
    {
        var ingredients = lines.Select(line => new Ingredient(line)).ToList();
        var recipes = PopulateRecipes(ingredients, [new Recipe()]);
        return isFirstSolution ? recipes.Max(recipe => recipe.Score()) 
        : recipes.Where(recipe => recipe.Calories == 500).Max(recipe => recipe.Score());
    }

    private static List<Recipe> PopulateRecipes(List<Ingredient> ingredients, List<Recipe> recipes)
    {
        if (ingredients.Count == 1)
        {
            recipes.ForEach(
                recipe => recipe.Ingredients.Add(ingredients.First(), 100 - recipe.Ingredients.Values.Sum()));
            return recipes;
        }

        var result = new List<Recipe>();
        foreach (var recipe in recipes)
        {
            for (var i = 0; i <= 100 - recipe.Ingredients.Values.Sum(); i++)
                result.Add(new Recipe(
                    new Dictionary<Ingredient, int>(recipe.Ingredients) { { ingredients.First(), i } }));
        }

        return PopulateRecipes(ingredients[1..], result);
    }

    private class Ingredient
    {
        public Ingredient(string input)
        {
            var chunks = input.Split(" ").Select(chunk => chunk.Trim(',')).ToList();
            Name = chunks[0];
            Capacity = int.Parse(chunks[2]);
            Durability = int.Parse(chunks[4]);
            Flavor = int.Parse(chunks[6]);
            Texture = int.Parse(chunks[8]);
            Calories = int.Parse(chunks[10]);
        }

        public string Name { get; }
        public int Capacity { get; }
        public int Durability { get; }
        public int Flavor { get; }
        public int Texture { get; }
        public int Calories { get; }
    }

    private class Recipe
    {
        public Recipe()
        {
            Ingredients = new Dictionary<Ingredient, int>();
        }
        
        public Recipe(Dictionary<Ingredient, int> ingredients)
        {
            Ingredients = ingredients;
        }

        public Dictionary<Ingredient, int> Ingredients { get; set; }
        
        public int Calories =>  Ingredients.Sum(item => item.Value * item.Key.Calories);
        public long Score()
        {
            long total = 1;
            var score = Ingredients.Sum(item => item.Value * item.Key.Capacity);
            if (score < 1) return 0;
            total *= score;
            score = Ingredients.Sum(item => item.Value * item.Key.Durability);
            if (score < 1) return 0;
            total *= score;
            score = Ingredients.Sum(item => item.Value * item.Key.Flavor);
            if (score < 1) return 0;
            total *= score;
            score = Ingredients.Sum(item => item.Value * item.Key.Texture);
            if (score < 1) return 0;
            return total * score;
        }
    }
}