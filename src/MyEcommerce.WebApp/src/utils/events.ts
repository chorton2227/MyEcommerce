const addEvent = (
  eventType: string,
  listener: EventListenerOrEventListenerObject
) => {
  document.addEventListener(eventType, listener);
};

const removeEvent = (
  eventType: string,
  listener: EventListenerOrEventListenerObject
) => {
  document.removeEventListener(eventType, listener);
};

const addOneTimeEvent = (
  eventType: string,
  listener: EventListenerOrEventListenerObject
) => {
  const handleOneTimeEvent = (event: Event) => {
    const listenerObject = listener as EventListenerObject;
    const listenerCallable = listener as EventListener;

    if (listenerObject.handleEvent) {
      listenerObject.handleEvent(event);
    } else {
      listenerCallable(event);
    }

    removeEvent(eventType, handleOneTimeEvent);
  };

  addEvent(eventType, handleOneTimeEvent);
};

const triggerEvent = (eventType: string, data?: any) => {
  const event = new CustomEvent(eventType, { detail: data });
  document.dispatchEvent(event);
};

export { addEvent, removeEvent, addOneTimeEvent, triggerEvent };
