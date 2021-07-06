using TMPro;
using UnityEngine;
namespace ShootEmUp.UI
{
    public class InputFieldValidation:MonoBehaviour
    {
        public TMP_InputField mainInputField=null;
        [SerializeField]
        private int _characterLimit=3;
        private bool _isThereCommaAlready = false;
        public void Start()
        {
            mainInputField = GetComponent<TMP_InputField>();
            mainInputField.onValidateInput+= (input, charIndex, addedChar) => MyValidate(addedChar);
            mainInputField.characterLimit=_characterLimit;
        }
        private char MyValidate(char charToValidate)
        {
            const string validcharacters = "1234567890,";

            if(validcharacters.IndexOf(charToValidate)!=-1)
            {
                if ((validcharacters.IndexOf(charToValidate)==10) &&(!_isThereCommaAlready))
                {
                    _isThereCommaAlready = true;
                }
                return charToValidate;
            }
            return'\0';


        }
    }
}
        
    

