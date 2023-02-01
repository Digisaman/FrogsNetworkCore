using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrogsNetwork.Freelancing.Models;
public enum LanguageLevel
{
    [Display( Name = "Native Speaker")]
    Native,

    [Display(Name = "Near native / fluent")]
    Fluent,

    [Display(Name = "Excellent command / highly proficiency")]
    Proficient,

    [Display(Name = "Very good command")]
    Advanced,

    [Display(Name = "Good command / good working knowledge")]
    Good,

    [Display(Name = " Basic communication skills / working knowledge")]
    Basic
    
}
