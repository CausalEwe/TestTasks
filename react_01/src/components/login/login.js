import React from 'react';

const login = (props) => {
  let currentFromProps = props.data.currentLanguage;
  let currentLanguage = props.data.languages.russian;
  if (currentFromProps != 'russian') {
    currentLanguage = props.data.languages.english;
  }
  return <div>{currentLanguage.registration}</div>;
};
export default login;
