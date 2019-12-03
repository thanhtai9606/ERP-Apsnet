using ERPLibrary.Helpers;
using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ERPSystem.Core.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ERPDatabaseEntities Context;
        OperationResult operationResult = new OperationResult();
        public Repository(ERPDatabaseEntities context)
        {
            this.Context = context;
        }
        public TEntity GetEntity(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }
        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public OperationResult Add(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Add(entity);
                operationResult.Success = true;
                operationResult.Message = "Đã thêm";
                operationResult.Caption = "Success";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Có lỗi xảy ra: " + ex.ToString();
                operationResult.Caption = "Error";
            }

            return operationResult;

        }
        public OperationResult AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().AddRange(entities);
                operationResult.Success = true;
                operationResult.Message = "Đã thêm vào danh sách";
                operationResult.Caption = "Success";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Có lỗi xảy ra: " + ex.ToString();
                operationResult.Caption = "Error";
            }

            return operationResult;

        }
        public OperationResult Update(TEntity entity, Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                TEntity existObject = FindBy(predicate).FirstOrDefault();
                if (existObject != null)
                {
                    Context.Entry(existObject).CurrentValues.SetValues(entity);
                }
                operationResult.Success = true;
                operationResult.Message = "Đã cập nhật thành công";
                operationResult.Caption = "Success";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Có lỗi xảy ra: " + ex.ToString();
                operationResult.Caption = "Error";
            }

            return operationResult;

        }
        public OperationResult Remove(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
                operationResult.Success = true;
                operationResult.Message = "Đã xóa thành công";
                operationResult.Caption = "Success";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Có lỗi xảy ra: " + ex.ToString();
                operationResult.Caption = "Error";
            }

            return operationResult;

        }
        public OperationResult RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Set<TEntity>().RemoveRange(entities);
                operationResult.Success = true;
                operationResult.Message = "Đã xóa thành công";
                operationResult.Caption = "Success";
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Có lỗi xảy ra: " + ex.ToString();
                operationResult.Caption = "Error";
            }

            return operationResult;

        }
    }
}