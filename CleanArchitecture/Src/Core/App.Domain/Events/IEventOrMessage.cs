namespace App.Domain.Events;

public interface IEventOrMessage : IMessage, IEvent; //where koşulunda or yok and var olan iki interface i tek bir interface de birleştirelim
