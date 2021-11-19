const isAuth = (): boolean => {
  const item = localStorage.getItem("jwt");
  if (!item) {
    return false;
  }

  return item.length > 0;
};

export default isAuth;
