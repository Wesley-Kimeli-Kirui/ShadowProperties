using Microsoft.EntityFrameworkCore;
using ShadowProperties;

using var db = new Db(); // Creating a new instance of the DbContext named 'db'
await db.Database.EnsureCreatedAsync(); // Ensuring that the database schema is created (if it doesn't exist)

User user = new() // Creating a new User object and initializing it using object initializer syntax
{
    Name = "Wesley", // Setting the 'Name' property of the User object
    Tests = new Test[] // Initializing the 'Tests' property as an array of Test objects
    {
        new() { Text = "Understanding Shadow Properties" } // Creating a new Test object with a 'Text' property
    }
};

db.Users.Add(user); // Adding the User object to the 'Users' DbSet in the DbContext

await db.SaveChangesAsync(); // Saving changes to the database

var test = await db.Tests.FirstAsync(); // Retrieving the first Test object asynchronously from the 'Tests' DbSet

var userId = db.Entry(test).Property<int>("UserId").CurrentValue; // Getting the 'UserId' property value of the 'test' object using shadow property

Console.WriteLine($"user: {userId} tested: \"{test.Text}\""); // Printing the user and test information to the console

var tests = db.Tests.Where(x => EF.Property<int>(x, "UserId") == 1).ToArray();
// Querying the 'Tests' DbSet to find Test objects where the 'UserId' shadow property is equal to 1 and converting the result to an array

Console.WriteLine(tests[0].Text); // Printing the 'Text' property of the first Test object in the 'tests' array to the console
