-- Insert two fake flash card stacks
INSERT INTO stacks (name)
VALUES 
('Geography'),
('Mathematics');

-- Get the IDs of the newly inserted stacks
DECLARE @GeographyStackId INT;
DECLARE @MathematicsStackId INT;

SELECT @GeographyStackId = Id FROM stacks WHERE name = 'Geography';
SELECT @MathematicsStackId = Id FROM stacks WHERE name = 'Mathematics';

-- Insert 10 fake flashcards for the Geography stack
INSERT INTO flashcards (front, back, StackId)
VALUES 
('What is the capital of France?', 'Paris', @GeographyStackId),
('What is the largest continent?', 'Asia', @GeographyStackId),
('What is the longest river?', 'Nile', @GeographyStackId),
('What country has the most population?', 'China', @GeographyStackId),
('What is the smallest country?', 'Vatican City', @GeographyStackId),
('What is the largest desert?', 'Sahara', @GeographyStackId),
('Which ocean is the deepest?', 'Pacific Ocean', @GeographyStackId),
('Which mountain is the highest?', 'Mount Everest', @GeographyStackId),
('What is the coldest continent?', 'Antarctica', @GeographyStackId),
('What is the capital of Japan?', 'Tokyo', @GeographyStackId);

-- Insert 10 fake flashcards for the Mathematics stack
INSERT INTO flashcards (front, back, StackId)
VALUES 
('What is 2 + 2?', '4', @MathematicsStackId),
('What is the square root of 16?', '4', @MathematicsStackId),
('What is 5 x 6?', '30', @MathematicsStackId),
('What is 12 divided by 3?', '4', @MathematicsStackId),
('What is the formula for the area of a circle?', 'πr^2', @MathematicsStackId),
('What is the value of pi (π)?', '3.14159', @MathematicsStackId),
('What is the derivative of x^2?', '2x', @MathematicsStackId),
('What is the sum of angles in a triangle?', '180 degrees', @MathematicsStackId),
('What is 7 squared?', '49', @MathematicsStackId),
('What is the Pythagorean theorem?', 'a^2 + b^2 = c^2', @MathematicsStackId);
