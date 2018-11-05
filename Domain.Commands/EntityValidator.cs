using Domain.Commands.Contracts;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Commands
{
    public abstract class EntityValidator<TEntity> 
        : AbstractValidator<TEntity>, IEntityValidator<TEntity>
        where TEntity : class
    {
        public string Error { get; private set; }

        public bool IsValid(TEntity entity)
        {
            var result = Validate(entity);

            if (result.IsValid)
            {
                Error = null;
                return true;
            }

            Error = ConvertErrors(result.Errors);

            throw new Exception(Error);
        }

        private static string ConvertErrors(IEnumerable<ValidationFailure> fails)
        {
            var sb = new StringBuilder();

            foreach (var fail in fails)
            {
                sb.AppendLine(fail.ToString());
            }

            return sb.ToString();
        }
    }
}
