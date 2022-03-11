﻿namespace MPP_lab_project.Repository.Interfaces;

public interface IRepository<Tid, T>
{
    void Add(T elem);
    void Delete(T elem);
    void Update(T elem, Tid id);
    T FindById(Tid id);
}