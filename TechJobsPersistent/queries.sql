--Part 1
int Id
string Name
int EmployerId

--Part 2
SELECT Name FROM Employers
WHERE location='St. Louis City';

--Part 3
SELECT Name, Description FROM skills
WHERE Description IS NOT NULL
ORDER BY Name ASC, Description ASC;
