package repository;

import java.util.Collection;

/**
 * Abstract interface for repository objects.
 * @param <Tid> Type of ID of the objects stored in repository
 * @param <T> Type of the objects stored in repository
 */
public interface Repository<Tid, T> {
    void add(T elem);
    void delete(T elem);
    void update(T elem, Tid id);
    T findById(Tid id);
}
