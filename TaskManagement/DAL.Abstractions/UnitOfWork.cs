using System;
using Microsoft.EntityFrameworkCore;
using Models;
using Type = Models.Type;

namespace DAL.Abstractions
{
    public class UnitOfWork : IDisposable
    {
        private readonly DbContext _context;
        private IRepository<AssignedTask> _assignedTaskRepository;
        private IRepository<Complexity> _complexityRepository;
        private IRepository<Employee> _employeeRepository;
        private IRepository<EventLog> _eventLogRepository;
        private IRepository<Qualification> _qualificationRepository;
        private IRepository<Result> _resultRepository;
        private IRepository<Status> _statusRepository;
        private IRepository<Task> _taskRepository;
        private IRepository<Type> _typeRepository;
        private bool _disposed;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IRepository<AssignedTask> AssignedTaskRepository => _assignedTaskRepository ?? (_assignedTaskRepository = new IRepository<AssignedTask>(_context));

        public IRepository<Complexity> ComplexityRepository => _complexityRepository ?? (_complexityRepository = new IRepository<Complexity>(_context));

        public IRepository<Employee> EmployeeRepository => _employeeRepository ?? (_employeeRepository = new IRepository<Employee>(_context));

        public IRepository<EventLog> EventLogRepository => _eventLogRepository ?? (_eventLogRepository = new IRepository<EventLog>(_context));

        public IRepository<Qualification> QualificationRepository => _qualificationRepository ?? (_qualificationRepository = new IRepository<Qualification>(_context));

        public IRepository<Result> ResultRepository => _resultRepository ?? (_resultRepository = new IRepository<Result>(_context));

        public IRepository<Status> StatusRepository => _statusRepository ?? (_statusRepository = new IRepository<Status>(_context));

        public IRepository<Task> TaskRepository => _taskRepository ?? (_taskRepository = new IRepository<Task>(_context));

        public IRepository<Type> TypeRepository => _typeRepository ?? (_typeRepository = new IRepository<Type>(_context));
        
        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}