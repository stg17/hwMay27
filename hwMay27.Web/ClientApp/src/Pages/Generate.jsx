import axios from "axios";
import { useState } from "react";


const Generate = () => {

    const [amount, setAmount] = useState(0);

    const onGenerateClick = async () => {
        window.location.href = `/api/person/generate?amount=${amount}`;
    }

    return (<div className="row">
        <div className="mt-5 d-flex w-100 justify-content-center align-self-center">
            <div className="row">
                <input className="form-control-lg" type="text" placeholder="Amount"
                    name="amount" value={amount}  onChange={e => setAmount(e.target.value)} />
            </div>
            <div className="row">
                <div className="col-md-4 offset-md-2">
                    <button onClick={onGenerateClick} className="btn btn-primary btn-lg">Generate</button>
                </div>
            </div>
        </div>
    </div>
    )
}

export default Generate;