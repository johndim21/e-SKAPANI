//
//  DigitalWalkChooserView.swift
//  Escapani
//
//  Created by Panos Peltekis on 13/4/21.
//

import UIKit

protocol DigitalWalkChooserViewDelegate: AnyObject {
    func setSightInfo(_ sender: UIButton)
}

class DigitalWalkChooserView: UIView {
    
    // MARK: - IBOutlets
    
    @IBOutlet var view: UIView!
    
    @IBOutlet weak var personImageFrameView: UIView! {
        didSet {
            self.personImageFrameView.backgroundColor = .white
            self.personImageFrameView.layer.cornerRadius = personImageFrameView.frame.width / 2
        }
    }
    
    @IBOutlet weak var personImageView: UIImageView! {
        didSet {
            self.personImageView.image = UIImage(named: "walking_man_black_icon")
        }
    }
    
    @IBOutlet weak var rotontaButton: UIButton!
    @IBOutlet weak var rotontaLabel: UILabel!
    @IBOutlet weak var archOfGaleriusButton: UIButton!
    @IBOutlet weak var archOfGaleriusLabel: UILabel!
    @IBOutlet weak var ippodromosButton: UIButton!
    @IBOutlet weak var ippodromosLabel: UILabel!
    @IBOutlet weak var octagonButton: UIButton!
    @IBOutlet weak var octagonLabel: UILabel!
    @IBOutlet weak var vasilikiButton: UIButton!
    @IBOutlet weak var vasilikiLabel: UILabel!
    @IBOutlet weak var sightsNamesStackView: UIStackView!
    
    @IBOutlet weak var startupLabel: UILabel! {
        didSet {
            self.startupLabel.textAlignment = .center
            self.startupLabel.text = ANLocalizedString("digital_walk_chooser_info_label", comment: "")
        }
    }
    
    @IBOutlet weak var sightInfoView: UIView! {
        didSet {
            self.sightInfoView.backgroundColor = UIColor(red: 71, green: 90, blue: 191)
            self.sightInfoView.isHidden = true
        }
    }
    
    @IBOutlet weak var sightInfoImageView: UIImageView! {
        didSet {
            self.sightInfoImageView.contentMode = .scaleAspectFill
            self.sightInfoImageView.image = UIImage(named: "info_icon_white")
            self.sightInfoImageView.backgroundColor = .clear
        }
    }
    
    @IBOutlet weak var sightInfoLabel: UILabel! {
        didSet {
            self.sightInfoLabel.textColor = .white
        }
    }
    
    //MARK: - IBActions
    
    @IBAction func digitalWalkSightButtonTap(_ sender: UIButton) {
        delegate?.setSightInfo(sender)
        switch sender {
        case self.rotontaButton:
            self.setupSightInfo(.rotonda, ANLocalizedString("rotunda_description_label", comment: ""))
        case self.archOfGaleriusButton:
            self.setupSightInfo(.archOfGalerius, ANLocalizedString("arch_of_galerius_description_label", comment: ""))
        case self.ippodromosButton:
            self.setupSightInfo(.hippodrome, ANLocalizedString("hippodrome_description_label", comment: ""))
        case self.octagonButton:
            self.setupSightInfo(.octagon, ANLocalizedString("octagon_description_label", comment: ""))
        case self.vasilikiButton:
            self.setupSightInfo(.vasiliki, ANLocalizedString("vasiliki_description_label", comment: ""))
        default:
            break
        }
    }
    
    // MARK: - Properties
    
    weak var delegate: DigitalWalkChooserViewDelegate?
    var currentSelectedSite: DigitalWalkSightType?
    
    // MARK: - Init
    
    override init(frame: CGRect) {
        super.init(frame: frame)
        commonInit()
    }
    
    required init?(coder: NSCoder) {
        super.init(coder: coder)
        commonInit()
    }
    
    override func awakeFromNib() {
        super.awakeFromNib()
        
        backgroundColor = .clear
        
        self.rotontaLabel.text = ""
        self.archOfGaleriusLabel.text = ""
        self.ippodromosLabel.text = ""
        self.octagonLabel.text = ""
        self.vasilikiLabel.text = ""
    }
    
    override func layoutSubviews() {
        super.layoutSubviews()
        
        rotontaButton.setTitle("", for: .normal)
        rotontaButton.setImage(UIImage(named: "rotonta_icon"), for: .normal)
        rotontaButton.imageView?.contentMode = .scaleAspectFit
        rotontaButton.imageEdgeInsets = UIEdgeInsets(top: 10, left: 10, bottom: 10, right: 10)
        rotontaButton.adjustsImageWhenHighlighted = false
        
        archOfGaleriusButton.setTitle("", for: .normal)
        archOfGaleriusButton.setImage(UIImage(named: "arch_of_galerius_icon"), for: .normal)
        archOfGaleriusButton.imageView?.contentMode = .scaleAspectFit
        archOfGaleriusButton.imageEdgeInsets = UIEdgeInsets(top: 10, left: 10, bottom: 10, right: 10)
        archOfGaleriusButton.adjustsImageWhenHighlighted = false
        
        ippodromosButton.setTitle("", for: .normal)
        ippodromosButton.setImage(UIImage(named: "ippodromos_icon"), for: .normal)
        ippodromosButton.imageView?.contentMode = .scaleAspectFit
        ippodromosButton.imageEdgeInsets = UIEdgeInsets(top: 10, left: 10, bottom: 10, right: 10)
        ippodromosButton.adjustsImageWhenHighlighted = false
        
        octagonButton.setTitle("", for: .normal)
        octagonButton.setImage(UIImage(named: "oktagwno_icon"), for: .normal)
        octagonButton.imageView?.contentMode = .scaleAspectFit
        octagonButton.imageEdgeInsets = UIEdgeInsets(top: 10, left: 10, bottom: 10, right: 10)
        octagonButton.adjustsImageWhenHighlighted = false
        
        vasilikiButton.setTitle("", for: .normal)
        vasilikiButton.setImage(UIImage(named: "vasiliki_icon"), for: .normal)
        vasilikiButton.imageView?.contentMode = .scaleAspectFit
        vasilikiButton.imageEdgeInsets = UIEdgeInsets(top: 10, left: 10, bottom: 10, right: 10)
        vasilikiButton.adjustsImageWhenHighlighted = false
    }
    
    // MARK: - Methods
    
    func setupSightInfo(_ sightType: DigitalWalkSightType, _ infoText: String) {
        
        if !startupLabel.isHidden {
            startupLabel.isHidden = true
            sightInfoView.isHidden = false
        }
        
        if currentSelectedSite != nil {
            switch currentSelectedSite {
            case .rotonda:
                rotontaButton.setImage(UIImage(named: "rotonta_icon"), for: .normal)
                rotontaLabel.text = ""
            case .archOfGalerius:
                archOfGaleriusButton.setImage(UIImage(named: "arch_of_galerius_icon"), for: .normal)
                archOfGaleriusLabel.text = ""
            case .hippodrome:
                ippodromosButton.setImage(UIImage(named: "ippodromos_icon"), for: .normal)
                ippodromosLabel.text = ""
            case .octagon:
                octagonButton.setImage(UIImage(named: "oktagwno_icon"), for: .normal)
                octagonLabel.text = ""
            case .vasiliki:
                vasilikiButton.setImage(UIImage(named: "vasiliki_icon"), for: .normal)
                vasilikiLabel.text = ""
            case .none:
                break
            }
        }
        
        switch sightType {
        case .rotonda:
            rotontaButton.setImage(UIImage(named: "rotonta_button_icon"), for: .normal)
            rotontaLabel.text = ANLocalizedString("rotunda_title_label", comment: "Rotonda Title Label")
            currentSelectedSite = .rotonda
        case .archOfGalerius:
            archOfGaleriusButton.setImage(UIImage(named: "arch_of_galerius_button_icon"), for: .normal)
            archOfGaleriusLabel.text = ANLocalizedString("arch_of_galerius_title_label", comment: "Arch of Galerius Title Label")
            currentSelectedSite = .archOfGalerius
        case .hippodrome:
            ippodromosButton.setImage(UIImage(named: "ippodromos_button_icon"), for: .normal)
            ippodromosLabel.text = ANLocalizedString("hippodrome_title_label", comment: "Hippodrome Title Label")
            currentSelectedSite = .hippodrome
        case .octagon:
            octagonButton.setImage(UIImage(named: "oktagwno_button_icon"), for: .normal)
            octagonLabel.text = ANLocalizedString("octagon_title_label", comment: "Octagon Title Label")
            currentSelectedSite = .octagon
        case .vasiliki:
            vasilikiButton.setImage(UIImage(named: "vasiliki_button_icon"), for: .normal)
            vasilikiLabel.text = ANLocalizedString("vasiliki_title_label", comment: "Vasiliki Title Label")
            currentSelectedSite = .vasiliki
        }
        sightInfoLabel.text = infoText
    }
    
    func commonInit() {
        let viewFromXib = Bundle.main.loadNibNamed("DigitalWalkChooserView", owner: self, options: nil)![0] as! UIView
        viewFromXib.frame = self.bounds
        addSubview(viewFromXib)
    }
}
