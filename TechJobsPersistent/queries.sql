--Part 1
int Id
varchar Name
int EmployerId

--Part 2
SELECT Name FROM Employers
WHERE location='St. Louis City';

--Part 3
SELECT Name, Description FROM skills
LEFT JOIN jobskills on skills.id = jobskills.skillsid
WHERE jobskills.jobid IS NOT NULL
ORDER BY Name ASC, Description ASC;
