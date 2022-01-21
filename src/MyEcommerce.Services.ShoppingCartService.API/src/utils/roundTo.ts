const roundTo = (num: number, places: number) => {
  const factor = 10 ** places;
  return Math.round(num * factor) / factor;
};

export default roundTo;
