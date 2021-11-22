//
//  DigitalWalkViewController.swift
//  
//
//  Created by Panagiotis Theodosiadis on 29/1/21.
//

import UIKit

protocol DigitalWalkViewControllerDelegate: AnyObject {
    func moveToSightDetailsView(_ currentSight: DigitalWalkSightType)
}

class DigitalWalkViewController: BaseViewController, ViewModelable {
    
    // MARK: - ViewModelable
    
    var viewModel: DigitalWalkViewModel!
    
    // MARK: - Properties
    
    var rotondaButton: UIButton = UIButton()
    var archOfGaleriusButton: UIButton = UIButton()
    var hippodromeButton: UIButton = UIButton()
    var octagonButton: UIButton = UIButton()
    var vasilikiButton: UIButton = UIButton()
    
    weak var delegate: DigitalWalkViewControllerDelegate?
    
    let transition = BottomToTopAnimator()
    
    // MARK: - IBOutlets
    
//    @IBOutlet weak var rotontaButton: UIButton!
//    @IBOutlet weak var archOfGaleriusButton: UIButton!
//    @IBOutlet weak var hipodromosButton: UIButton!
//    @IBOutlet weak var octagonButton: UIButton!
//    @IBOutlet weak var vasilikiButton: UIButton!
    @IBOutlet weak var moveToSightDetailsButton: UIButton!
    
    @IBOutlet weak var digitalWalkChooserView: DigitalWalkChooserView!
    
    @IBAction func digitalWalkSightButtonTap(_ sender: UIButton) {
        setupView(sender)
    }
    
    @IBAction func moveToSightDetailsButtonTap(_ sender: Any) {
        guard let currentSight = viewModel.route else { return }
        self.delegate?.moveToSightDetailsView(currentSight)
    }
    
    // MARK: - Lifecycle
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        view.backgroundColor = .clear
        view.isOpaque = false
        
        archOfGaleriusButton.setImage(UIImage(named: "arch_of_galerius_button_icon"), for: .normal)
        archOfGaleriusButton.setTitle("", for: .normal)
        archOfGaleriusButton.layer.opacity = 0
        hippodromeButton.setImage(UIImage(named: "ippodromos_button_icon"), for: .normal)
        hippodromeButton.setTitle("", for: .normal)
        hippodromeButton.layer.opacity = 0
        octagonButton.setImage(UIImage(named: "oktagwno_button_icon"), for: .normal)
        octagonButton.setTitle("", for: .normal)
        octagonButton.layer.opacity = 0
        vasilikiButton.setImage(UIImage(named: "vasiliki_button_icon"), for: .normal)
        vasilikiButton.setTitle("", for: .normal)
        vasilikiButton.layer.opacity = 0
        
        rotondaButton = UIButton(type: .custom)
        rotondaButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.12, y: UIScreen.main.bounds.height * 0.18, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        rotondaButton.setImage(UIImage(named: "rotonta_button_icon"), for: .normal)
        rotondaButton.setTitle("", for: .normal)
        rotondaButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        rotondaButton.layer.opacity = 0
        self.view.addSubview(rotondaButton)
        
        archOfGaleriusButton = UIButton(type: .custom)
        archOfGaleriusButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.22, y: UIScreen.main.bounds.height * 0.23, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        archOfGaleriusButton.setImage(UIImage(named: "arch_of_galerius_button_icon"), for: .normal)
        archOfGaleriusButton.setTitle("", for: .normal)
        archOfGaleriusButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        archOfGaleriusButton.layer.opacity = 0
        self.view.addSubview(archOfGaleriusButton)
        
        hippodromeButton = UIButton(type: .custom)
        hippodromeButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.55, y: UIScreen.main.bounds.height * 0.28, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        hippodromeButton.setImage(UIImage(named: "ippodromos_button_icon"), for: .normal)
        hippodromeButton.setTitle("", for: .normal)
        hippodromeButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        hippodromeButton.layer.opacity = 0
        self.view.addSubview(hippodromeButton)
        
        octagonButton = UIButton(type: .custom)
        octagonButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.14, y: UIScreen.main.bounds.height * 0.3, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        octagonButton.setImage(UIImage(named: "oktagwno_button_icon"), for: .normal)
        octagonButton.setTitle("", for: .normal)
        octagonButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        octagonButton.layer.opacity = 0
        self.view.addSubview(octagonButton)
        
        vasilikiButton = UIButton(type: .custom)
        vasilikiButton.frame = CGRect(x: UIScreen.main.bounds.width * 0.2, y: UIScreen.main.bounds.height * 0.35, width: UIScreen.main.bounds.height * 0.06, height: UIScreen.main.bounds.height * 0.06)
        vasilikiButton.setImage(UIImage(named: "vasiliki_button_icon"), for: .normal)
        vasilikiButton.setTitle("", for: .normal)
        vasilikiButton.addTarget(self, action: #selector(updateSightsViews(sender:)), for: .touchUpInside)
        vasilikiButton.layer.opacity = 0
        self.view.addSubview(vasilikiButton)
        
        digitalWalkChooserView.delegate = self
        digitalWalkChooserView.personImageFrameView.transform = CGAffineTransform(translationX: -(UIScreen.main.bounds.width * 0.3), y: 0)
        digitalWalkChooserView.transform = CGAffineTransform(translationX: 0, y: UIScreen.main.bounds.height)
        
        moveToSightDetailsButton.transform = CGAffineTransform(rotationAngle: -.pi / 2)
        moveToSightDetailsButton.transform = CGAffineTransform(translationX: 0, y: UIScreen.main.bounds.height)
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
        UIView.animate(withDuration: 0.5) {
            
            self.rotondaButton.layer.opacity = 1
            self.archOfGaleriusButton.layer.opacity = 1
            self.hippodromeButton.layer.opacity = 1
            self.octagonButton.layer.opacity = 1
            self.vasilikiButton.layer.opacity = 1
            self.digitalWalkChooserView.transform = CGAffineTransform(translationX: 0, y: 0)
        }
    }
    
    override func viewDidLayoutSubviews() {
        super.viewDidLayoutSubviews()
        
        moveToSightDetailsButton.adjustsImageWhenHighlighted = false
        moveToSightDetailsButton.setTitle("", for: .normal)
        moveToSightDetailsButton.setBackgroundImage(UIImage(named: "double_arrow_icon"), for: .normal)
        moveToSightDetailsButton.imageView?.contentMode = .scaleAspectFill
        moveToSightDetailsButton.imageEdgeInsets = UIEdgeInsets(top: -10, left: -10, bottom: -10, right: -10)
        moveToSightDetailsButton.tintColor = .white
        moveToSightDetailsButton.backgroundColor = UIColor(red: 0.03, green: 0.69, blue: 0.90, alpha: 1.00)
        moveToSightDetailsButton.layer.cornerRadius = moveToSightDetailsButton.frame.width / 2
        digitalWalkChooserView.view.layer.cornerRadius = 10
    }
    
    // MARK: - Methods
    
    @objc
    func updateSightsViews(sender: UIButton) {
        setupView(sender)
    }
    
    private func setupView(_ sender: UIButton) {
        if moveToSightDetailsButton.transform != CGAffineTransform(translationX: 0, y: 0) {
            UIView.animate(withDuration: 0.75) {
                self.moveToSightDetailsButton.transform = CGAffineTransform(translationX: 0, y: 0)
            }
        }
        switch sender {
        case self.rotondaButton, self.digitalWalkChooserView.rotontaButton:
            break
            self.digitalWalkChooserView.setupSightInfo(.rotonta, "Rotonda")
        case self.archOfGaleriusButton, self.digitalWalkChooserView.archOfGaleriusButton:
            self.viewModel.setCurrentSight(.archOfGalerius)
            self.digitalWalkChooserView.setupSightInfo(.archOfGalerius, ANLocalizedString("arch_of_galerius_description_label", comment: ""))
        case self.hippodromeButton, self.digitalWalkChooserView.ippodromosButton:
            break
            self.digitalWalkChooserView.setupSightInfo(.hippodrome, "Ippodromos")
        case self.octagonButton, self.digitalWalkChooserView.octagonButton:
            break
            self.digitalWalkChooserView.setupSightInfo(.octagon, "Octagon")
        case self.vasilikiButton, self.digitalWalkChooserView.vasilikiButton:
            break
            self.digitalWalkChooserView.setupSightInfo(.vasiliki, "Vasiliki")
        default:
            break
        }
    }
    
}

extension DigitalWalkViewController: Storyboardable {
    static var storyboardName: String = "DigitalWalk"
    static var storyboardIdentifier: String? = "DigitalWalkViewControllerIdentifier"
}

extension DigitalWalkViewController: DigitalWalkChooserViewDelegate {
    func setSightInfo(_ sender: UIButton) {
        setupView(sender)
    }
}
