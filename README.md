# Hey everyone!

We’re kicking off our coding with a cool project where you'll get to build and enhance a console application that handles RPG character data, focusing on file input/output operations. This will get your hands dirty with some essential C# skills, including reading from and writing to files.

## Here's what you need to do:

### Build a Simple Console Application
- Start with the console application that I’ve provided in GitHub Classroom. It’s set up to get you going with basic user input and output.
- Make sure your app prompts for character details and displays them nicely.

### Add File I/O Operations
- Extend your application to interact with data from a text file named `input.csv`.
- Your task is to read character data, update it (think leveling up), and write the changes back to the same file (`input.csv`).
- Check GitHub Classroom for starting hints and snippets!

### Example Menu
1. Display Characters
2. Add Character
3. Level Up Character

## Notes:
For a basic C# console application that reads from and writes to a CSV file, the program hierarchy will be quite straightforward.

```
YourProjectName/
├── Program.cs          # Main C# file containing all the logic for the application
└── input.csv           # Text file containing initial data for the application to process
```

## Grading Rubric:
- **0%** if the program doesn’t run at all.
- **50%** if the program runs but you miss out on some logic.
- **95%** if you nail all the requirements.
- **5%** for clean, well-documented code.

### Stretch Goal - +10%:
- Implement the CsvHelper library to refine how you handle CSV files. This is a great opportunity to see how libraries can streamline your code.
- If you decide to implement the stretch goal with CsvHelper, you might also have a `packages.config` file or similar dependencies managed via your project’s solution file if using Visual Studio.

## Submission Details:
- Accept the assignment template in GitHub Classroom, update, and submit your code.
- Provide your GitHub submission URL in the Canvas assignment.
- Make sure everything is submitted by the due date.

## Need Help?
If you’re stuck or have questions, post them in our Canvas discussion board or bring them up during our next session on Thursday.
