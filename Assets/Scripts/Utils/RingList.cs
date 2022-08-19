using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RingList<T> : ICollection<T>, IEnumerator<T>
{
    private int _index = 0;

    [SerializeField]
    private T _preferredItem;

    [SerializeField]
    private bool _isVirgin = true;

    [SerializeField]
    private List<T> _list = new List<T>();

    public int Count { get { return _list.Count; } }
    public bool IsReadOnly { get { return false; } }

    private T _current;
    public T Current
    {
        get
        {
            if (this._isVirgin && this.Contains(_preferredItem))
            {
                this._isVirgin = false;
                this._index = this._list.IndexOf(_preferredItem);
                this._current = this._list[this._index];

                return this._current;
            }
            return _current;
        }
        private set
        {
            _current = value;
        }
    }

    public RingList()
    {
        _preferredItem = default(T);
    }
    /// <summary>
    /// Конструктор передаёт предпочитаемый элемент, который затем при первом вызове "Current" будет выбран.
    /// </summary>
    /// <param name="preferredItem"> Предпочитаемый элемент. </param>
    public RingList(T preferredItem)
    {
        this._preferredItem = preferredItem;
    }

    public T this[int index]
    {
        get
        {
            return _list[index];
        }

        set
        {
            _list[index] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return this;
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    object IEnumerator.Current
    {
        get { return Current; }
    }

    public void Add(T item)
    {
        _list.Add(item);

        Current = _list[_list.Count - 1];
        _index = _list.Count - 1;
    }
    public void Clear()
    {
        _list.Clear();
    }
    public bool Contains(T item)
    {
        return _list.Contains(item);
    }
    public void CopyTo(T[] array, int arrayIndex)
    {
        _list.CopyTo(array, arrayIndex);
    }

    public void AddRange(List<T> array)
    {
        _list.AddRange(array);
    }

    public bool Remove(T item)
    {
        return _list.Remove(item);
    }
    public bool RemoveAt(int index)
    {
        if (index < _list.Count)
        {
            _list.RemoveAt(index);
            return true;
        }
        else
            return false;
    }

    public bool MoveNext()
    {
        if (_list.Count == 0)
            return false;
        _index = _list.Count > _index + 1 ? _index + 1 : 0;
        Current = _list[_index];
        return true;
    }
    public bool MovePrev()
    {
        if (_list.Count == 0)
            return false;
        _index = _index - 1 > -1 ? _index - 1 : _list.Count - 1;
        Current = _list[_index];
        return true;
    }

    public void Reset()
    {
        _index = 0;
        Current = _list[_index];
    }

    public void Dispose() {}

    public override string ToString()
    {
        
        string result = string.Empty;
        if (_list.Count == 0)
            return "The list is empty.";

        for (int i = 0; i < _list.Count; i++)
            result += $"[{i}] - {_list[i].ToString()} \n";
        return result;
    }
}
