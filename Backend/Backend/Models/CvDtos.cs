namespace Backend.Models
{
    public class CvDto
    {
        public BasicsDto Basics { get; set; }
        public List<EducationDto> Education { get; set; }
        public List<WorkDto> Work { get; set; }
        public List<SkillDto> Skills { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public List<CertificateDto> Certificates { get; set; }
        public List<PublicationDto> Publications { get; set; }
        public List<AwardDto> Awards { get; set; }
        public List<ProjectDto> Projects { get; set; }
        public List<InterestDto> Interests { get; set; }
        public List<ReferenceDto> References { get; set; }
        public List<VolunteerDto> Volunteer { get; set; }
    }

    public class BasicsDto
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public List<ProfileDto> Profiles { get; set; }
    }

    public class ProfileDto
    {
        public string Network { get; set; }
        public string Url { get; set; }
    }

    public class EducationDto
    {
        public string Institution { get; set; }
        public string Area { get; set; }
        public string StudyType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Score { get; set; }
    }

    public class WorkDto
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Summary { get; set; }
    }

    public class SkillDto
    {
        public string Name { get; set; }
        public string Level { get; set; }
        public string Keywords { get; set; }
    }

    public class LanguageDto
    {
        public string Language { get; set; }
        public string Fluency { get; set; }
    }

    public class CertificateDto
    {
        public string Name { get; set; }
        public string Issuer { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
    }

    public class PublicationDto
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string ReleaseDate { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
    }

    public class AwardDto
    {
        public string Title { get; set; }
        public string Awarder { get; set; }
        public string Date { get; set; }
        public string Summary { get; set; }
    }

    public class ProjectDto
    {
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
    }

    public class InterestDto
    {
        public string Name { get; set; }
    }

    public class ReferenceDto
    {
        public string Name { get; set; }
        public string Reference { get; set; }
    }

    public class VolunteerDto
    {
        public string Organization { get; set; }
        public string Position { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Summary { get; set; }
    }
}
