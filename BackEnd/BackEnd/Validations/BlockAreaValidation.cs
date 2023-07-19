using BackEnd.Models;
using BackEnd.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class BlockAreaValidation :AbstractValidator<BlockArea>
    {
        public BlockAreaValidation()
        {
            RuleFor(c => c.AreaId).NotEmpty().NotNull().WithMessage("Area Name Must not be null ");
            RuleFor(c => c.DateOfBlock).NotEmpty().NotNull().WithMessage("Date of Blocked Area Musn't be null");
            RuleFor(c => c.Note).NotEmpty().NotNull().WithMessage("Note Must not be null ");
        }
    }
    public class listOFBlockAreaValidation : AbstractValidator<ListofBlockAreas>
    {
        public listOFBlockAreaValidation()
        {
            RuleForEach(c => c.BlockAreas).SetValidator(new BlockAreaValidation());
           
        }
    }
}
